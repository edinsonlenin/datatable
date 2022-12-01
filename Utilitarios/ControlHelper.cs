using System.Web.UI.WebControls;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    public static class ControlHelper
    {
        public static void NewReadOnly(this WebControl control, bool value)
        {
            if (value)
                control.Attributes.Add("readonly", "readonly");
            else
                control.Attributes.Remove("readonly");
        }
    }
}