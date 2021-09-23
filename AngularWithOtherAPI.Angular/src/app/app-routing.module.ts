import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditStudentComponent } from './component/student/add-edit-student/add-edit-student.component';
import { StudentComponent } from './component/student/student.component';

const routes: Routes = [
  { component: StudentComponent, path: 'student-list' },
  { component: AddEditStudentComponent, path: 'student-list/add' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
