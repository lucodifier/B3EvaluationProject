import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ReactiveFormsModule } from '@angular/forms';
import { InvestmentService } from '../investment.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-investment-calculator',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './investment-calculator.component.html',
})
export class InvestmentCalculatorComponent {
  investmentForm: FormGroup;
  result: any;
  errorMessage: string | null = null;
  isSubmitted = false;
  backendErrors: string[] = [];

  constructor(private fb: FormBuilder, private investmentService: InvestmentService) {
    this.investmentForm = this.fb.group({
      initialValue: [null, [Validators.required, Validators.min(0.01)]],
      months: [null, [Validators.required, Validators.min(1)]]
    });
  }

  get initialValue(): AbstractControl | null {
    return this.investmentForm.get('initialValue');
  }

  get months(): AbstractControl | null {
    return this.investmentForm.get('months');
  }

  calculate() {
    this.isSubmitted = true;
    this.errorMessage = null;
    this.backendErrors = [];

    if (this.investmentForm.invalid) {
      return;
    }

    this.investmentService.calculateInvestment(this.investmentForm.value).subscribe({
      next: data => {
        this.result = data;
      },
      error: error => {
        console.error('Error calculating investment', error);
        this.handleBackendErrors(error.error);
      }
    });
  }

  private handleBackendErrors(errorResponse: any) {
    if (errorResponse?.errors) {
      for (const key in errorResponse.errors) {
          this.backendErrors.push(...errorResponse.errors[key]);
      }
    } else {
      this.errorMessage = 'Ocorreu um erro ao calcular o investimento.';
    }
  }
}
