﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineBanking.Application.Resources.Success {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SuccessMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SuccessMessage() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OnlineBanking.Application.Resources.Success.SuccessMessage", typeof(SuccessMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Деньги на счёт добавлены.
        /// </summary>
        internal static string AddMoneyToAccountMessage {
            get {
                return ResourceManager.GetString("AddMoneyToAccountMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ваш кредит полностью погашен!.
        /// </summary>
        internal static string CloseCredit {
            get {
                return ResourceManager.GetString("CloseCredit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Транзакция была успешно выполнена.
        /// </summary>
        internal static string CompleteTransaction {
            get {
                return ResourceManager.GetString("CompleteTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Счёт был успешно создан.
        /// </summary>
        internal static string CreateAccountMessage {
            get {
                return ResourceManager.GetString("CreateAccountMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Кредит был успешно создан.
        /// </summary>
        internal static string CreateCreditMessage {
            get {
                return ResourceManager.GetString("CreateCreditMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ваш счёт успешно удалён!.
        /// </summary>
        internal static string DeleteAccountMessage {
            get {
                return ResourceManager.GetString("DeleteAccountMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Карта была успешно привязана к счёту, вы можете забрать её в ближашем пункте выдачи!.
        /// </summary>
        internal static string LinkCardMessage {
            get {
                return ResourceManager.GetString("LinkCardMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Карта была успешно удалена, больше данная карта не валидна для этого счёта!.
        /// </summary>
        internal static string UnlinkCardMessage {
            get {
                return ResourceManager.GetString("UnlinkCardMessage", resourceCulture);
            }
        }
    }
}
