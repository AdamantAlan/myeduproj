import { EventEmitter, Input, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { todosAr } from '../app.component';

@Component({
  selector: 'app-pag2test',
  templateUrl: './pag2test.component.html',
  styleUrls: ['./pag2test.component.scss']
})
export class Pag2testComponent implements OnInit {

  constructor() { }
  @Input() todos21: todosAr[]
  @Output() onToggle = new EventEmitter<number>()
  ngOnInit(): void {
  }
  onChange(id: number) {
    this.onToggle.emit(id)
  }
}
