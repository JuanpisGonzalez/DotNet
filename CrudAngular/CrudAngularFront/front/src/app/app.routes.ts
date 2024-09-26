import { Routes } from '@angular/router';
import { InitComponent } from './pages/init/init.component';
import { EmployeeComponent } from './pages/employee/employee.component';

export const routes: Routes = [
    {path:'',component:InitComponent},
    {path:'init',component:InitComponent},
    {path:'Employee/:id',component:EmployeeComponent}
];
