﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TwemojiMinecraftGen.Properties {
    using System;
    
    
    /// <summary>
    ///   Třída prostředků se silnými typy pro vyhledávání lokalizovaných řetězců atp.
    /// </summary>
    // Tato třída byla automaticky generována třídou StronglyTypedResourceBuilder
    // pomocí nástroje podobného aplikaci ResGen nebo Visual Studio.
    // Chcete-li přidat nebo odebrat člena, upravte souboru .ResX a pak znovu spusťte aplikaci ResGen
    // s parametrem /str nebo znovu sestavte projekt aplikace Visual Studio.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Vrací instanci ResourceManager uloženou v mezipaměti použitou touto třídou.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TwemojiMinecraftGen.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Potlačí vlastnost CurrentUICulture aktuálního vlákna pro všechna
        ///   vyhledání prostředků pomocí třídy prostředků se silnými typy.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Vyhledá lokalizovaný řetězec podobný {
        ///  &quot;providers&quot;: [
        ///    {
        ///      &quot;type&quot;: &quot;bitmap&quot;,
        ///      &quot;file&quot;: &quot;minecraft:font/%name%.png&quot;,
        ///      &quot;ascent&quot;: 7,
        ///      &quot;chars&quot;: [
        ///        %chars%
        ///      ]
        ///    }
        ///  ]
        ///}.
        /// </summary>
        public static string default_json {
            get {
                return ResourceManager.GetString("default_json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Vyhledá lokalizovaný řetězec podobný {
        ///	&quot;pack&quot;: {
        ///		&quot;pack_format&quot;: 6,
        ///		&quot;description&quot;: [
        ///			{
        ///				&quot;text&quot;: &quot;Autogenerated from: &quot;,
        ///				&quot;bold&quot;: true,
        ///				&quot;color&quot;: &quot;#FF0000&quot;
        ///			},
        ///			{
        ///				&quot;text&quot;: &quot;%github_link%&quot;,
        ///				&quot;bold&quot;: false,
        ///				&quot;underlined&quot;: true,
        ///				&quot;color&quot;: &quot;#0000EE&quot;
        ///			}
        ///		]
        ///	}
        ///}.
        /// </summary>
        public static string pack_mcmeta {
            get {
                return ResourceManager.GetString("pack_mcmeta", resourceCulture);
            }
        }
    }
}
