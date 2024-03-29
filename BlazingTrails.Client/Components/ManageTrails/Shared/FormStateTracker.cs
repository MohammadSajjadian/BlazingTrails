using BlazingTrails.Application.DTOs;
using BlazingTrails.Client.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Components.ManageTrails.Shared
{
    public class FormStateTracker : ComponentBase
    {
        [Inject]
        public AppState appState { get; set; }

        [CascadingParameter]
        private EditContext cascadedEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (cascadedEditContext is null)
                throw new InvalidOperationException($"{nameof(FormStateTracker)} requires a cascading parameter of type {nameof(EditContext)}");

            cascadedEditContext.OnFieldChanged += CascadedEditContextOnFieldChanged;
        }

        private void CascadedEditContextOnFieldChanged(object sender, FieldChangedEventArgs args)
        {
            var trail = (TrailDto)args.FieldIdentifier.Model;
            if (trail.Id == 0)
            {
                appState.SaveTrail(trail);
            }
        }
    }
}
