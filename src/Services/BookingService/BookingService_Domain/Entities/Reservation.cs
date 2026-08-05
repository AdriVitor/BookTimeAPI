using BookingService_Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingService_Domain.Entities
{
    [Table("reservation")]
    public class Reservation
    {
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("id_resource", TypeName = "int")]
        [Required]
        public int IdResource { get; set; }
        [Column("id_customer", TypeName = "int")]
        [Required]
        public int IdCustomer { get; set; }
        [Column("startdate", TypeName = "timestamp without time zone")]
        [Required]
        public DateTime StartDate { get; set; }
        [Column("enddate", TypeName = "timestamp without time zone")]
        [Required]
        public DateTime EndDate { get; set; }
        [Column("observation", TypeName = "varchar(255)")]
        public string Observation { get; set; }
        [Column("status", TypeName = "int")]
        [Required]
        public int Status { get; set; }
        [Column("js_config", TypeName = "varchar(255)")]
        public string? JsConfig { get; set; }

        #region SagaValidation
        [JsonIgnore]
        [NotMapped]
        public bool? UserValidated { get; set; }
        [JsonIgnore]
        [NotMapped]
        public bool? ResourceValidated { get; set; }

        public void SetUserValidation(bool isValid) => UserValidated = isValid;
        public void SetResourceValidation(bool isAvailable) => ResourceValidated = isAvailable;

        public void MarkAsConfirmed() => Status = (int)StatusReservationEnum.Confirmed;
        public void MarkAsFailed() => Status = (int)StatusReservationEnum.Failed;

        public void UpdateSchedule(DateTime startDate, DateTime endDate, string observation)
        {
            ValidateData(IdResource, IdCustomer, startDate, endDate, observation, Status);

            StartDate = startDate;
            EndDate = endDate;
            Observation = observation;
        }

        public Reservation()
        {
            
        }

        public Reservation(int idResource, 
                           int idCustomer, 
                           DateTime startDate, 
                           DateTime endDate,
                           string observation,
                           int? status = null,
                           int? id = null
                           )
        {
            ValidateData(idResource, idCustomer, startDate, endDate, observation, status);

            IdResource = idResource;
            IdCustomer = idCustomer;
            StartDate = startDate;
            EndDate = endDate;
            Observation = observation;
            if (status != null) Status = (int)status;
            if (id != null) Id = (int)id;
        }

        public void ValidateData(
                           int idResource,
                           int idCustomer,
                           DateTime startDate,
                           DateTime endDate,
                           string observation,
                           int? status)
        {
            if (idResource <= 0) throw new Exception("O recurso informado é inválido.");

            if (idCustomer <= 0) throw new Exception("O cliente informado é inválido.");

            if (startDate == default) throw new Exception("A data de início é obrigatória.");

            if (endDate == default) throw new Exception("A data de término é obrigatória.");

            if (endDate <= startDate) throw new Exception("A data de término deve ser posterior à data de início.");

            if (startDate.Date == endDate.Date) throw new Exception("As reservam devem ter no mínimo um dia de duração");

            if (observation?.Length > 255) throw new Exception("A observação deve conter no máximo 255 caracteres.");

            if(status != null)
                if (!Enum.IsDefined(typeof(StatusReservationEnum), status)) throw new Exception("Escolha um status válido");
        }

        public void ValidateData()
        {
            ValidateData(IdResource, IdCustomer, StartDate, EndDate, Observation, Status);
        }
        #endregion
    }
}
