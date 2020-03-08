namespace FlightManager.Common
{
    public static class NotificationMessages
    {
        public static string SuccessfullyLoggedOut => "Успешно излизане от профила Ви";
        public static string UserAccountUpdated => "Успешна актуализиция на част от основната информация в профила Ви";
        public static string UserAccountCreated => "Успешно добавяне на служител";
        public static string UserAccountCreatError => "Неуспешно добавяне на служител";
        
        public static string UserAccountDeleted => "Успешно изтриване на профил";
        public static string UserAccountDeleteError => "Неуспешно изтриване на профил";
        public static string UserAccountEdited => "Успешно редактиране на данните за потребителя";
        public static string UserAccountEditError => "Неуспешно редактиране на данните за потребителя";
        public static string UserSuccessfullyChangedPassword => "Успешна промяна на паролата";
        public static string UserChangePasswordError => "Неуспешна промяна на паролата";
        public static string FlightCreated => "Успешно създаване на полет";
        public static string FlightCreateError => "Неуспешно създаване на полет";
        public static string FlightEdited => "Успешна промяна на данните за полета";
        public static string FlightEditError => "Неуспешна промяна на данните за полета";
        public static string FlightDeleted => "Успешно изтриване на полета";
        public static string FlightDeleteError => "Неуспешно изтриване на полета";
        
        public static string ReservationSuccess => "Успешна резервация. Моля проверете своя имейл за потвърждение";
        public static string ReservationError => "Неуспешна резервация. Местата са заети";
        public static string ReservationDeleted => "Успешно изтриване на резервация";
        public static string ReservationDeleteError => "Неуспешно изтриване на резервация";
        
        public static string PasswordIncorrect => "Неправилна парола";
    }
}