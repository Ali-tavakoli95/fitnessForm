import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FitnessFormService } from 'src/app/services/fitness-form.service';
import { FitUser } from 'src/models/fit-user.model';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent {
  userId!: string;
  userDetails!: FitUser;
  constructor(private activatedRoute: ActivatedRoute, private fitnessForimService: FitnessFormService) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(val => {
      this.userId = val['id'];
      this.fetchUserDetails(this.userId);
    })
  }

  fetchUserDetails(userId: string) {
    this.fitnessForimService.getRegisteredUserId(userId)
      .subscribe({
        next: (res) => {
          this.userDetails = res;
        },
        error: (err) => {
          console.log(err);
        }
      })
  }
}
