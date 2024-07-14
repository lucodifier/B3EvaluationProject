import { Routes } from '@angular/router';
import { InvestmentCalculatorComponent } from './investment-calculator/investment-calculator.component';

export const routes: Routes = [
  { path: '', redirectTo: '/investment-calculator', pathMatch: 'full' },
  { path: 'investment-calculator', component: InvestmentCalculatorComponent }
];
