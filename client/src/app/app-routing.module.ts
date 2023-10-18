import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { UserDetailComponent } from './components/user-detail/user-detail.component';
import { RegistrationListComponent } from './components/registration-list/registration-list.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { NoAccessComponent } from './components/no-access/no-access.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'update/:id', component: RegisterComponent },
  { path: 'detail/:id', component: UserDetailComponent },
  { path: 'list', component: RegistrationListComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'no-access', component: NoAccessComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
