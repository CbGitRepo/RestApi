import { IClient } from "../interfaces";
import { HttpDataService } from "../service/HttpDataService";
import { OnInit, Component } from "@angular/core";
import { error } from "protractor";

@Component({
  selector: 'clients',
  templateUrl: './clients.component.html'

})
export class ClientComponent implements OnInit {

  title: string;
  clients: IClient[] = [];
  filteredClients: IClient[] = [];
  _listFilter = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredClients = this.listFilter ? this.performFilter(this.listFilter) : this.clients;
  }
  totalRecords: number = 0;
  pageSize: number = 10;

  constructor(
    private dataService: HttpDataService) { }
  performFilter(filterBy: string): IClient[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.clients.filter((client: IClient) =>
      client.FirstName.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }
  ngOnInit() {
    this.title = 'Customers';
    this.getCustomers();
  }
  getCustomers() {
    this.dataService.getClients().subscribe((clients: IClient[]) => {
      this.clients = this.filteredClients = clients;
    }, (error: any) => console.log(error),
      () => console.log('error get clients'));
  }
}

