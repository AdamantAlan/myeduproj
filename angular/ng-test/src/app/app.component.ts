import { Component } from '@angular/core';


export interface todosAr {
  id: number
  name: string
  chek: boolean
}



@Component({
      selector: 'app-root',
      templateUrl: './app.component.html',
      styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ng-test';
  public todos321: todosAr[] = [{id:1, name: `${this.title}`,chek:false }, { id:2, name: "otherName", chek:true }]

  onToglle(id: number) {
    // console.log(id)
    const idx = this.todos321.findIndex((t) => t.id === id)
    this.todos321[idx].chek = !this.todos321[idx].chek;

  }


}
