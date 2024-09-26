import { Component, inject, Input, input, OnInit } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeService } from '../../Services/employee.service';
import { Router } from '@angular/router';
import { Employee } from '../../models/Employee';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, MatFormFieldModule, MatButtonModule, MatInputModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {

  @Input('id') IdEmployee!: number;
  private employeeService = inject(EmployeeService);
  public formBuild = inject(FormBuilder);

  public formEmployee: FormGroup = this.formBuild.group({
    FullName: [''],
    Email: [''],
    Salary: [0],
    ContractDate: ['']
  });

  constructor(private router: Router) {

  }

  ngOnInit(): void {
    if (this.IdEmployee != 0) {
      this.employeeService.Get(this.IdEmployee).subscribe({
        next: (data) => {
          this.formEmployee.patchValue({
            FullName: data.FullName,
            Email: data.Email,
            Salary: data.Salary,
            ContractDate: data.ContractDate
          })
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }

  Save() {
    const object: Employee = {
      IdEmployee: this.IdEmployee,
      FullName: this.formEmployee.value.FullName,
      Email: this.formEmployee.value.Email,
      Salary: this.formEmployee.value.Salary,
      ContractDate: this.formEmployee.value.ContractDate

    }

    if (this.IdEmployee == 0) {
      this.employeeService.Create(object).subscribe({
        next: (data) => {
          if(data.isSuccess){
            this.router.navigate(['/']);
          }else{
            alert("Error creating a new employee")
          }
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }else{
      this.employeeService.Update(object).subscribe({
        next: (data) => {
          if(data.isSuccess){
            this.router.navigate(['/']);
          }else{
            alert("Error updating a new employee")
          }
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }

  ComeBack(){
    this.router.navigate(['/']);
  }

}
