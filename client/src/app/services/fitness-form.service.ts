import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FitUser } from 'src/models/fit-user.model';

@Injectable({
  providedIn: 'root'
})
export class FitnessFormService {

  constructor(private http: HttpClient) { }

  postRegistration(registerObj: FitUser) {
    return this.http.post<FitUser>("https://localhost:5001/api/fitness/register", registerObj);
  }

  getRegisteredUser() {
    return this.http.get<FitUser[]>("https://localhost:5001/api/fitness/get-all");
  }

  getRegisteredUserId(id: string) {
    return this.http.get<FitUser>("http://localhost:5000/api/fitness/get-fit-user-by-id/" + id);
  }

  updateRegisterUser(registerObj: FitUser, id: string) {
    return this.http.put<FitUser>("https://localhost:5001/api/fitness/update/" + id, registerObj);
  }

  deleteRegistered(id: string) {
    return this.http.delete<FitUser>("https://localhost:5001/api/fitness/delete/" + id);
  }

}
