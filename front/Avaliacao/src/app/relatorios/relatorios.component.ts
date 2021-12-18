import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-relatorios',
  templateUrl: './relatorios.component.html',
  styleUrls: ['./relatorios.component.css']
})
export class RelatoriosComponent implements OnInit {

  titulo = 'Usuários';

  public relatorios = [
      { id: 1, nome : 'Junior', nascimento : '10/09/1990',  email : 'jn@mail.com' },
      { id: 2, nome : 'Paula', nascimento : '10/09/1992',  email : 'paula@mail.com' },
  ];


  constructor() { }

  ngOnInit() {
  }

}
