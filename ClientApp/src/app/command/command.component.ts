import { Component } from '@angular/core';
import { IClient, ICammande } from "../interfaces";

import { ICommandName } from 'selenium-webdriver';
import { Route, ActivatedRoute } from '@angular/router';
import { HttpDataService } from '../service/HttpDataService';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-command-list-component',
  templateUrl: './command.component.html'
})
export class CommandComponent {
  objClient: IClient;
  commandes: ICammande[];
  constructor(private dataService: HttpDataService, private router: Router, private route: ActivatedRoute)
  {

  }
  performFilter(filterBy: number): ICammande[] {
    return this.commandes.filter((commande: ICammande) =>
      commande.id == filterBy);
  }
  ngOnInit()
  {
    const param = this.route.snapshot.paramMap.get('id');
    if (param) {
      const id = +param;
      this.dataService.getClient(id).subscribe(client => this.objClient=client);
    }
  }
  OnBack(): void
  {
    this.router.navigate(['/clients']);
  }
}
