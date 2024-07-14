import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { InvestmentCalculatorComponent } from './investment-calculator/investment-calculator.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    InvestmentCalculatorComponent
  ],
  template: '<app-investment-calculator></app-investment-calculator>',  
})
export class AppComponent {
  title = 'B3EvaluationProject.FrontEnd';
}
