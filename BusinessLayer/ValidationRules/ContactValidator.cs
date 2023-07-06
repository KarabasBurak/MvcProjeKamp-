using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator:AbstractValidator<Contact>
    {
        public ContactValidator() 
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Lütfen geçerli bir mail adresi giriniz");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu kısmını boş geçemezsiniz");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adını boş geçemezsiniz");
            RuleFor(x => x.UserName).MinimumLength(50).WithMessage("Lütfen en az 3 karakter girişi yapınız");
        }
    }
}
