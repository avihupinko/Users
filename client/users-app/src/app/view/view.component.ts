import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoaderService } from 'src/shared/loader.service';
import { User } from 'src/shared/models/user';
import { UsersService } from 'src/shared/users.service';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent {
  public user?: User;
  constructor(
    private service: UsersService,
    private loader: LoaderService,
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router,
  ) {
    this.loader.setLoading(true);
    let id = this.route.snapshot.paramMap.get('id');
    if (!!id) {
      this.service.getById(id).subscribe(user => {
        this.user = user;
        this.loader.setLoading(false);
      })
    } else {
      this.back();
    }
  }

  back() {
    this.router.navigate(['/']);
  }
}
