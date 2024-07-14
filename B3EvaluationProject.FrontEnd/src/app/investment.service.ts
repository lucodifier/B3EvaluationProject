import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvestmentService {
  private apiUrl = 'https://localhost:5001/api/investment'; // Atualize a URL conforme necessário

  constructor(private http: HttpClient) { }

  calculateInvestment(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, data);
  }
}
