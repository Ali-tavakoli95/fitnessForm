import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { FitnessFormService } from 'src/app/services/fitness-form.service';
import { FitUser } from 'src/models/fit-user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  selectedGender!: string;
  genders: string[] = ["مرد", "زن"];
  trainerOpts: string[] = ["آره", "نه"];
  packages: string[] = ["ماهانه", "سه ماه یکبار", "سالانه"];
  importantList: string[] = [
    "کاهش چربی سمی",
    "انرژی و استقامت",
    "ساخت عضلات لاغر",
    "سیستم گوارش سالم تر",
    "بدن هوس قند",
    "تناسب اندام"
  ];
  beenGyms: string[] = ["آره", "نه"];

  registerForm!: FormGroup;
  private userIdToUpdate!: string;
  public isUpdateActive: boolean = false;

  constructor(private fb: FormBuilder, private fitnessFormService: FitnessFormService, private toastService: NgToastService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      mobile: ['', Validators.required],
      weight: ['', Validators.required],
      height: ['', Validators.required],
      bmi: ['', Validators.required],
      bmiResult: ['', Validators.required],
      gender: ['', Validators.required],
      requireTrainer: ['', Validators.required],
      package: ['', Validators.required],
      important: ['', Validators.required],
      haveGymBefore: ['', Validators.required],
      enquiryDate: ['', Validators.required]
    });

    this.registerForm.controls['height'].valueChanges.subscribe(res => {
      this.calculateBmi(res);
    });

    this.activatedRoute.params.subscribe(val => {
      this.userIdToUpdate = val['id'];
      this.fitnessFormService.getRegisteredUserId(this.userIdToUpdate)
        .subscribe(res => {
          this.isUpdateActive = true;
          this.FillFormToUpdate(res);
        })
    })
  }

  submit() {
    this.fitnessFormService.postRegistration(this.registerForm.value)
      .subscribe(res => {
        this.toastService.success({ detail: "موفقیت", summary: "فرم اضافه شد", duration: 3000 });
        this.registerForm.reset();
      })
  }

  update() {
    this.fitnessFormService.updateRegisterUser(this.registerForm.value, this.userIdToUpdate)
      .subscribe(res => {
        this.toastService.success({ detail: "موفقیت", summary: "فرم به روز شد", duration: 3000 });
        this.registerForm.reset();
        this.router.navigate(['list'])
      })
  }

  calculateBmi(heightValue: number) {
    const weight = this.registerForm.value.weight;
    const height = heightValue;
    const bmi = weight / (height * height);
    this.registerForm.controls['bmi'].patchValue(bmi);
    switch (true) {
      case bmi < 18.5:
        this.registerForm.controls['bmiResult'].patchValue("کمبود وزن");
        break;
      case (bmi >= 18.5 && bmi < 25):
        this.registerForm.controls['bmiResult'].patchValue("طبیعی");
        break;
      case (bmi >= 25 && bmi < 30):
        this.registerForm.controls['bmiResult'].patchValue("اضافه وزن");
        break;

      default:
        this.registerForm.controls['bmiResult'].patchValue("چاق");
        break;
    }
  }

  FillFormToUpdate(user: FitUser) {
    this.registerForm.setValue({
      userName: user.userName,
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      mobile: user.mobile,
      weight: user.weight,
      height: user.height,
      bmi: user.bmi,
      bmiResult: user.bmiResult,
      gender: user.gender,
      requireTrainer: user.requireTrainer,
      package: user.package,
      important: user.important,
      haveGymBefore: user.haveGymBefore,
      enquiryDate: user.enquiryDate
    })
  }
}
