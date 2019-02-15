import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { IClient } from '../interfaces';


@Injectable()
export class HttpDataService {
  baseUrl: string = '/api/clients';

  constructor(private http: HttpClient)
  {

  }

  getClients(): Observable<IClient[]> {
    return this.http.get<IClient[]>(this.baseUrl)
      .pipe(
      map((clients: IClient[]) => {
        return clients;
        }),
        catchError(this.handleError)
      );
    ;
  }
  getClient(Id: number): Observable<IClient> {
    return this.getClients().pipe(
      map((clients: IClient[]) => clients.find(c => c.id == Id)));
   
  }
  private handleError(error: HttpErrorResponse) {
    console.error('server error:', error);
    if (error.error instanceof Error) {
      let errMessage = error.error.message;
      return Observable.throw(errMessage);
      // Use the following instead if using lite-server
      //return Observable.throw(err.text() || 'backend server error');
    }
    return Observable.throw(error || 'ASP.NET Core server error');
  }
}
