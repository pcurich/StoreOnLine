using System.ComponentModel.DataAnnotations;

namespace StoreOnLine.Infrastructure.Validation
{
    public class MustBeTrueAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool) value;
        }
    }
    //Se usa [MustBeTrue(ErrorMessage="Debe aceptar  los terminos")]
}