import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentComponent } from './student.component';
import { AddEditStudentComponent } from './add-edit-student/add-edit-student.component';

@NgModule({
  declarations: [
    StudentComponent,
    AddEditStudentComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    StudentComponent,
    AddEditStudentComponent
  ],
  providers: []
})
export class StudentModule { }
