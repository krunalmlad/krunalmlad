import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Student } from '../models/student';
import { DropdownListVM } from '../models/country';

@Injectable({
  providedIn: "root"
})
export class StudentService {

  baseUrl: string = "https://localhost:44301/api/";

  constructor(private http: HttpClient) {
    //this.baseUrl = @Inject('BASE_URL')
  }

  public getStudentList(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'Student/get-all-student').pipe(
      map((data: Student[]) => {
        return data;
      })
    );
  }

  public getStudentById(id: number): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + `Student/get-student-by-id/${id}`).pipe(
      map((data: Student) => {
        return data;
      })
    );
  }

  public addStudent(student: Student): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Student>(this.baseUrl + 'Student/add-student', JSON.stringify(student), httpOptions)
      .pipe();
  }

  public updateStudent(student: Student, studentId: number): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<Student>(this.baseUrl + `Student/update-student/${studentId}`, JSON.stringify(student), httpOptions)
      .pipe();
  }

  public deleteStudent(id?: number): Observable<any> {
    return this.http.delete(this.baseUrl + `Student/delete-student/${id}`)
      .pipe();
  }

  public getStandardList(): Observable<DropdownListVM[]> {
    return this.http.get<DropdownListVM[]>(this.baseUrl +"Standard/get-standard-dropdown-list").pipe(
      map((data: DropdownListVM[]) => {
        return data;
      })
    );
  }

  public getCountryList(): Observable<DropdownListVM[]> {
    return this.http.get<DropdownListVM[]>(this.baseUrl + "Country/get-country-dropdown-list").pipe(
      map((data: DropdownListVM[]) => {
        return data;
      })
    );
  }

}
