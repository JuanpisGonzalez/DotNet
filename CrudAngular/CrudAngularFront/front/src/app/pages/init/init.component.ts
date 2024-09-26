import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { EmployeeService } from '../../Services/employee.service';
import { Employee } from '../../models/Employee';
import { Router } from '@angular/router';


@Component({
  selector: 'app-init',
  standalone: true,
  imports: [MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule],
  templateUrl: './init.component.html',
  styleUrl: './init.component.css'
})
export class InitComponent {
  private employeeService = inject(EmployeeService);

  public EmployeeList: Employee[] = [];
  public displayedColumns: string[] = ['FullName','Email','Salary','ContractDate','Action'];

  constructor(private router: Router) { 
    this.GetEmployees();
  }

  GetEmployees() {
    this.employeeService.List().subscribe({
      next: (data) => {
        if (data.length > 0) {
          this.EmployeeList = data;
          console.log(this.EmployeeList);
        }
      },
      error: (err) => {
        console.log(err.message)
      }
    })
  }

  NewEmployee() {
    this.router.navigate(['/Employee', 0]);
  }

  Update(object:Employee) {
    this.router.navigate(['/Employee', object.IdEmployee])
  }

  Delete(object:Employee) {
    if (confirm("Are you sure to delete the employee " + object.FullName + "?")) {
      this.employeeService.Delete(object.IdEmployee).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.GetEmployees();
          } else {
            alert("it couldn´t delete")
          }
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }
}
