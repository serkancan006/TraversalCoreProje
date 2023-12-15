using DTOLayer.DTOs.AppUserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AppUSerRegisterValidator : AbstractValidator<AppUserRegisterDTO>
    {
        public AppUSerRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("ad alanı boş geçilmez.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("soyad alanı boş geçilmez.");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("mail alanı boş geçilmez.");
            RuleFor(x => x.Username).NotEmpty().WithMessage("kullanıcı adı alanı boş geçilmez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("şifre alanı boş geçilmez.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("şifre tekrar alanı boş geçilmez.");
            RuleFor(x => x.Username).MinimumLength(3).WithMessage("lütfen en az 5 karakter veri girişi yapınız.");
            RuleFor(x => x.Username).MaximumLength(20).WithMessage("lütfen en fazla 20 karakter veri girişi yapınız.");
            RuleFor(x => x.Password).Equal(y => y.ConfirmPassword).WithMessage("Şifreler uyuşmuyor");
        }
    }
}
