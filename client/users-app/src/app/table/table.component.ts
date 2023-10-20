import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/shared/models/user';
import { TableService } from './table.service';
import { Router } from '@angular/router';
import { UsersService } from 'src/shared/users.service';
import { LoaderService } from 'src/shared/loader.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  data$: Observable<User[]>;
  total$: Observable<number>;

  constructor(public service: TableService,
    private usersService: UsersService,
    private loader: LoaderService,
    private router: Router) {
    this.data$ = service.data$;
    this.total$ = service.total$;
  }
  ngOnInit(): void {
    this.service.refresh();
  }

  public view(id: string) {
    this.router.navigate([id, 'view'])
  }

  public edit(id: string) {
    this.router.navigate([id, 'edit'])
  }

  public create() {
    this.router.navigate(['create'])
  }

  public delete(id: string) {
    this.loader.setLoading(true);
    this.usersService.delete(id).subscribe(res => {
      this.loader.setLoading(false);
      window.alert('User deletd successfully');
      this.service.refresh();
    }, err => {
      this.loader.setLoading(false);
      window.alert(err.error);
    })
  }


}
