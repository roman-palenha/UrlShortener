import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.css']
})
export class AddFormComponent {
  url! : string;
  
  @Output() 
  passUrl: EventEmitter<string> = new EventEmitter();

  onSubmit() 
  { 
    this.passUrl.emit(this.url);
  }
}
