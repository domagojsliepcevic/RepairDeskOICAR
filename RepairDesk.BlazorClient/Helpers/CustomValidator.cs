using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace RepairDesk.BlazorClient.Helpers
{
    public class CustomValidator<T> : ComponentBase
    {
        [CascadingParameter] private EditContext CurrentEditContext { get; set; }
        [Parameter] public Expression<Func<T>> For { get; set; }
        [Parameter] public string ErrorMessage { get; set; }

        private ValidationMessageStore _messageStore;

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(CustomValidator<T>)} requires a cascading " +
                                                    $"parameter of type {nameof(EditContext)}. For example, you can use " +
                                                    $"{nameof(CustomValidator<T>)} inside an {nameof(EditForm)}.");
            }

            if (For == null)
            {
                throw new InvalidOperationException($"{nameof(CustomValidator<T>)} requires a value for the " +
                                                    $"'{nameof(For)}' parameter.");
            }

            _messageStore = new ValidationMessageStore(CurrentEditContext);
            CurrentEditContext.OnValidationRequested += (s, e) => this.Validate();
            CurrentEditContext.OnFieldChanged += (s, e) => _messageStore.Clear(e.FieldIdentifier);

            base.OnInitialized();
        }

        private void Validate()
        {
            var fieldName = (For.Body as MemberExpression)?.Member.Name;
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new InvalidOperationException($"The provided expression doesn't point to a valid property.");
            }

            var fieldIdentifier = new FieldIdentifier(CurrentEditContext.Model, fieldName);
            var value = For.Compile().Invoke();
            if (value == null || (value is int intValue && intValue <= 0))
            {
                _messageStore.Add(fieldIdentifier, ErrorMessage ?? $"Field '{fieldIdentifier.FieldName}' is invalid.");
                CurrentEditContext.NotifyValidationStateChanged();
            }
        }

        public void Dispose()
        {
            CurrentEditContext.OnValidationRequested -= (s, e) => this.Validate();
            CurrentEditContext.OnFieldChanged -= (s, e) => _messageStore.Clear(e.FieldIdentifier);
        }
    }

}
