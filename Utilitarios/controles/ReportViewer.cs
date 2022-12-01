using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using System.Drawing;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes
{
   public class ReportViewer : WebControl
   {
      #region Private Member Variables
      // Private members that will map to url access builder.
      private multiState _showToolbar = 0;
      private multiState _parameters = 0;
      private string _zoom = "100%";
      private string _searchstring = String.Empty;
      private string _searchnextstring = String.Empty;
      private string _reportParameters = String.Empty;
      private string _addFilter = String.Empty;
      private string _filterValues = String.Empty;
      private string _serverUrl = String.Empty;
      private string _url = String.Empty;
      private String _reportPath = String.Empty;
      private string _renderingFormat = "Default";
      private Hashtable _properties = new Hashtable();
      #endregion

      #region Public Enums for Property Browser
      public enum multiState {Default, True, False};
      #endregion

      #region Enum to String Conversion Arrays
      private string[] multiStateArray = {null, "true", "false"};
      #endregion

      #region Constructors
      public ReportViewer()
      {
      }

      #endregion

      #region Propertes
      [Browsable(true), ReadOnly(true),
      Category("General Report Parameters"),
      Description("Full report url.")]
      public string Url
      {
         get
         {
            return this._url;
         }
      }

      [Category("General Report Parameters"),
      Editor(typeof(Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes.Design.StringEditor),
         typeof(UITypeEditor)),
      Description("Server url such as http://localhost/reportserver")]
      public string ServerUrl
      {
         get
         {
            return this._serverUrl;
         }
         set
         {
            this._serverUrl = value;
            // Build the full url string when the property is set. 
            this.BuildUrlString();
         }
      }

      [Category("General Report Parameters"),
      Description("Report path such as /SampleReports/Company Sales")]
      public String ReportPath
      {
         get
         {
            return this._reportPath;
         }

         set
         {
            this._reportPath = value;
            // Build the full url string when the property is set.
            this.BuildUrlString();
         }
      }

      [Category("HTMLViewer Commands"),
      Description("Indicates whether to display the report toolbar.")]
      public multiState Toolbar
      {
         get
         {
            return this._showToolbar;
         }
         set
         {
            this._showToolbar = value;
            this.SetParameter("rc:toolbar", multiStateArray[(int)value]);
         }
      }

      [Category("HTMLViewer Commands"),
      Description("Indicates whether to show the parameters area of the toolbar.")]
      public multiState Parameters
      {
         get
         {
            return this._parameters;
         }
         set
         {
            this._parameters = value;
            this.SetParameter("rc:parameters", multiStateArray[(int)value]);
         }
      }

      [Category("HTMLViewer Commands"),
      Description("Sets the zoom property of the report"),
      Editor(typeof(Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes.Design.ZoomListboxTypeEditor), typeof(UITypeEditor))
      ]
      public string Zoom
      {
         get
         {
            return this._zoom;
         }
         set
         {
            this._zoom = value;
            this.SetParameter("rc:zoom", value);
         }
      }

      [Category("Rendering Format"),
      Editor(typeof(Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes.Design.FormatListboxTypeEditor), typeof(UITypeEditor)),
      Description("Sets the report rendering format.")
      ]
      public string Format
      {
         get
         {
            return this._renderingFormat;
         }

         set
         {
            this._renderingFormat = value;
            this.SetParameter("rs:Format", value);
         }
      }
      #endregion

      #region Methods
      /// <summary>
      /// Add or remove url access string properties. 
      /// </summary>
      /// <param name="name"></param>
      /// <param name="value"></param>
      private void SetParameter(string name, string value)
      {
         try
         {
            // Remove if value is null or empty. Value is null of the property grid value
            // is null or empty. Empty or null removes the property from the Hashtable.
            if(value == null | value == String.Empty )
            {
               this._properties.Remove(name);
            }
            else
            {
               if(this._properties.ContainsKey(name))
               {
                  // Change if key exists
                  this._properties[name] = value;
               }
               else
               {
                  // Add if key does not exist
                  this._properties.Add(name, value);
               }
            }
            // Build a new url string
            this.BuildUrlString();
         }
            // Catch and handle a more specific exception in a propduction application.
         catch(Exception ex)
         {
            // Sample throws the exception to the client
            throw ex;
         }
      }

      /// <summary>
      /// Enumerate Hashtable and create report server access specific string.
      /// </summary>
      /// <param name="properties"></param>
      /// <returns></returns>
      private string EmumProperties(Hashtable properties)
      {
         string paramsString = String.Empty;
         // Enumerate properties and create report server specific string.
         IDictionaryEnumerator customPropEnumerator = properties.GetEnumerator();
         while ( customPropEnumerator.MoveNext() )
         {
            paramsString += "&"
               + customPropEnumerator.Key
               + "=" + customPropEnumerator.Value;
         }

         return paramsString;
      }

      /// <summary>
      /// Add URL access command for rendering a report and any
      /// additional parameters.
      /// </summary>
      public string BuildUrlString()
      {
         this._url = this._serverUrl + "?" + this._reportPath +
            "&rs:Command=Render" + this.EmumProperties(this._properties);
         return this._url;
      }
      #endregion

      #region Render
      /// <summary>
      /// Render the report in an IFrame
      /// </summary>
      /// <param name="output"></param>
      protected override void Render(HtmlTextWriter output)
      {
         if (this._serverUrl == String.Empty || this._reportPath == String.Empty)
         {
            output.Write("<P style=\"font-family: Verdana; font-size: 11px\">");
            output.Write("To render a report, enter the ServerUrl and ReportPath.</P>");
         }
         else
         {
            // Create IFrame if the user enters ServerUrl and ReportPath
            output.WriteBeginTag("iframe");

            output.WriteAttribute("src", this.BuildUrlString());
            output.WriteAttribute("width", this.Width.ToString());
            output.WriteAttribute("height", this.Height.ToString());
            output.WriteAttribute("style", "border: 1 solid #C0C0C0");
            output.WriteAttribute("border", "0");
            output.WriteAttribute("frameborder", "0");

            output.Write(HtmlTextWriter.TagRightChar);
            output.WriteEndTag("iframe");
            output.WriteLine();
         }
      }

      #endregion
   }
}

