import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DropdownListVM } from '../../../models/country';
import { Student } from '../../../models/student';
import { StudentService } from '../../../services/student.service';

@Component({
  selector: 'app-add-edit-student',
  templateUrl: './add-edit-student.component.html'
  
})

export class AddEditStudentComponent implements OnInit, OnDestroy {

  public countryList?: DropdownListVM[] = [];
  public standardList?: DropdownListVM[] = [];
  //student: Student;
  pageTitle: string = 'Add Student';
  studentId: number = 0;


  constructor(private studentService: StudentService, private router: Router, private actionRoute: ActivatedRoute) {

  }



  ngOnInit() {
    //this.actionRoute.r
    this.actionRoute.params.subscribe(params => {
      const id = params['id'];
      if (id != undefined) {
        this.pageTitle = "Edit Student";
        this.studentId = Number(id);
        this.getStudentById(this.studentId);
      }
      //console.log('Url Id: ', id);
    });
    //this.student = new Student();
    this.getCountry();
    this.getStandards();
  }

  getStudentById(id: number) {
    this.studentService.getStudentById(id).subscribe((result: any) => {
      //this.student = result;
    });

  }

  getStandards() {
    this.studentService.getStandardList().subscribe((result: any) => {
      this.standardList = result;
    });
  }

  getCountry() {
    this.studentService.getCountryList().subscribe((result: any) => {
      this.countryList = result;
    });
  }

  onStudentSubmit(formData: any) {
    var student = formData.value;
    student.countryId = Number(student.countryId);
    student.standardId = Number(student.standardId);
    if (this.studentId && this.studentId > 0) {
      this.studentService.updateStudent(student, this.studentId).subscribe((result: any) => {
        this.router.navigateByUrl("/student-list");
      });
    }
    else {
      this.studentService.addStudent(student).subscribe((result: any) => {
        this.router.navigateByUrl("/student-list");
      });
    }
  }


  ngOnDestroy() {

  }


}
