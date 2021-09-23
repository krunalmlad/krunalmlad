import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StudentGridVM } from '../../models/student';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html'
})

export class StudentComponent implements OnInit, OnDestroy {

  studentList: StudentGridVM[] = [];

  constructor(private studentService: StudentService, private router: Router) {

  }

  ngOnInit() {
    this.bindGrid();
  }

  bindGrid() {
    this.studentService.getStudentList().subscribe((result) => {
      this.studentList = result;
    });
  }

  onEditStudent(id: number) {
    this.router.navigateByUrl("/student-list/edit/" + id);
  }

  onDeleteStudent(id?: number) {
    this.studentService.deleteStudent(id).subscribe((result: any) => {
      this.bindGrid();
    });
  }

  onAddStudentClick() {
    this.router.navigateByUrl("/student-list/add");
  }

  ngOnDestroy() {

  }

}
