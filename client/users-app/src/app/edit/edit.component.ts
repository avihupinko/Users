import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoaderService } from 'src/shared/loader.service';
import { UsersService } from 'src/shared/users.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent{
  public form?: FormGroup;
  public isEdit: boolean = false;
  constructor(
    private service: UsersService,
    private formBuilder: FormBuilder,
    private loader: LoaderService,
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router,
  ) {
    this.loader.setLoading(true);
    let id = this.route.snapshot.paramMap.get('id');
    if (!!id) {
      this.isEdit = true;
      this.service.getById(id).subscribe(user => {
        this.form = this.formBuilder.group({
          'id': this.formBuilder.control(user.id, {
            validators: [Validators.required]
          }),
          'userId': this.formBuilder.control(user.userId, {
            validators: [Validators.required, Validators.maxLength(250), Validators.pattern('[0-9]*')]
          }),
          'userName': this.formBuilder.control(user.userName, {
            validators: [Validators.required, Validators.maxLength(250)]
          }),
          'birthDate': this.formBuilder.control(this.datePipe.transform(user.birthDate, 'yyyy-MM-dd'), {
            validators: [Validators.required]
          }),
          'phone': this.formBuilder.control(user.phone, {
            validators: [Validators.maxLength(250), Validators.pattern('[0-9]*')]
          }),
          'email': this.formBuilder.control(user.email, {
            validators: [Validators.maxLength(250), Validators.email]
          }),
          'gender': this.formBuilder.control(user.gender, {}),
        })
        this.loader.setLoading(false);
      }, err => {
        this.router.navigate([]);
      })

    } else {
      this.isEdit = false;
      this.form = this.formBuilder.group({
        'userId': this.formBuilder.control('', {
          validators: [Validators.required, Validators.maxLength(250), Validators.pattern('[0-9]*')]
        }),
        'userName': this.formBuilder.control('', {
          validators: [Validators.required, Validators.maxLength(250)]
        }),
        'birthDate': this.formBuilder.control('', {
          validators: [Validators.required]
        }),
        'phone': this.formBuilder.control('', {
          validators: [Validators.maxLength(250), Validators.pattern('[0-9]*')]
        }),
        'email': this.formBuilder.control('', {
          validators: [Validators.maxLength(250), Validators.email]
        }),
        'gender': this.formBuilder.control(null, {}),
      });
      this.loader.setLoading(false);
    }
  }

  back(){
    this.router.navigate(['/']);
  }

  submit(){
    if(this.form?.valid){
      this.loader.setLoading(true);
      if(this.isEdit){
        this.service.update(this.form.getRawValue()).subscribe(res=>{
          this.loader.setLoading(false);
          this.router.navigate(['/'])
        }, err=>{
          window.alert(err.error);
          this.loader.setLoading(false);
        })
      }else{
        this.service.create(this.form.getRawValue()).subscribe(res=>{
          this.loader.setLoading(false);
          this.router.navigate(['/'])
        }, err=>{
          window.alert(err.error);
          this.loader.setLoading(false);
        })
      }
    }else{
      window.alert('Please fix form errors');
    }
  }
}
