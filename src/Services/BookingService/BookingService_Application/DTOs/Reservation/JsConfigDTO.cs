using Newtonsoft.Json;

namespace BookingService_Application.DTOs.Reservation
{
    public class JsConfigDTO
    {
        public bool UserValidated { get; set; }
        public bool ResourceValidated { get; set; }

        public void SetUserValidation(bool userValidated) => UserValidated = userValidated;

        public void SetResourceValidation(bool resourceValidated) => ResourceValidated = resourceValidated;

        public string Serialize(bool? userValidated = null, bool? resourceValidated = null)
        {
            if(userValidated != null)
                UserValidated = (bool)userValidated;

            if(resourceValidated != null)
                ResourceValidated = (bool)resourceValidated;

            return JsonConvert.SerializeObject(this);
        }
    }
}
