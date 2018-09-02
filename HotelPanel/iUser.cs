using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Constants;
using Data.DataModel;
using Logic.BusinessObjects;

namespace HotelPanel
{
    public class iUser
    {
        public static long CurrentUserId
        {
            get { try { return iUserStorage.Retrive<long>(PublicConstants.Session.CurrentUserId); } catch { return 0; } }
            set => iUserStorage.Store(PublicConstants.Session.CurrentUserId, value);
        }

        public static User1 CurrentUser
        {
            get => new PublicBusiness().GetUser(CurrentUserId);
            set => CurrentUserId = value.UserID;
        }


        public static long CurrentCurrencyId
        {
            get { try { return iUserStorage.Retrive<long>(PublicConstants.Session.CurrentCurrency); } catch { return 0; } }
            set => iUserStorage.Store(PublicConstants.Session.CurrentCurrency, value);
        }
        public static Currency CurrentCurrency
        {
            get => new PublicBusiness().GetCurrency(CurrentCurrencyId);
            set => CurrentCurrencyId = value.CurrencyID;
        }

        public static long CurrentLanguageId
        {
            get { try { return iUserStorage.Retrive<long>(PublicConstants.Session.CurrentLanguage); } catch { return 0; } }
            set => iUserStorage.Store(PublicConstants.Session.CurrentLanguage, value);
        }
        public static Data.DataModel.Language CurrentLanguage
        {
            get => new PublicBusiness().GetLanguage(CurrentLanguageId);
            set => CurrentLanguageId = value.LanguageID;
        }
    }
}