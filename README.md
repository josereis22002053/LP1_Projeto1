# Projeto 1 de Linguagens de Programação | 2020/2021 - Royal Game of Ur

## Autoria  

- Diogo Cruz - 22008318
  - Elaboração de métodos para as diversas classes
  - Manutenção da documentação
  - Elaboração do relatório

- José Reis - 22002053
  - Planeamento da estrutura do código
  - Elaboração de métodos para as diversas classes
  - Revisão do código e do relatório

- Tiago Berlim - 22006891
  - Elaboração de métodos para as diversas classes
  - Manutenção da documentação
  - Elaboração do relatório

## Arquitetura da solução

### Descrição da solução

Neste projeto foi utilizada a abordagem _Model-View-Controller_ (MVC).
O modelo é constituído pela classe `Board`. A _view_ é constituída pela classe
`ConsoleView` e o controlador é constituído pela classe `Controller`.

O modelo contém o estado do jogo e a condição de vitória, sendo esta conseguir
obter 7 pontos primeiro que o adversário.

A _view_ é responsável por demonstrar o estado do jogo e por pedir o _input_ dos
jogadores.

O controlador contém o _game loop_. É responsável por atualizar o estado do jogo
com base no _input_ recebido. Notifica também a _view_ quando é necessário
atualizar o que está a ser mostrado ou quando é necessário pedir _input_ aos
jogadores.

Ao iniciar a aplicação é apresentado um menu inicial onde são descritas as
regras do jogo e como se deve jogar. Depois começa então o _game loop_, sendo o
_Player1_ quem começa a jogar primeiro.

Relativamente à organização do código, para além das classes já mencionadas,
foi criada a _struct_ `Piece` e as enumerações `Fields`, `Players`, `SwapCodes`,
e `MessageCodes`. `Piece` representa as peças em jogo e contém apenas as
coordenadas da posição atual da peça e da posição para a qual essa peça se pode
mover, daí termos optado por uma _struct_ em vez de uma classe.  
Quanto às enumerações, `Fields` contém os diferentes tipos de campos que existem
no tabuleiro. `Players` representa os dois jogadores. `SwapCodes` possui os dois
códigos que podem ser passados ao método `Board.SwapTurn()`. Por fim,
`MessageCodes` contém os diferentes códigos que podem ser passados ao método
`ConsoleView.DisplayMessage()`, identificando que tipo de mensagem é que deve
ser mostrado.

Quanto ao _game loop_, cada turno começa por se desenhar o tabuleiro e dizer ao
jogador o resultado do lançamento dos dados. De seguida são mostradas as jogadas
válidas para cada peça, conforme o resultado obtido nos dados. Caso tenha sido
obtido um 0, o turno é passado ao jogador seguinte.

Para se obter os movimentos válidos para o jogador atual, primeiro é chamado o
método `Board.GetMoves()`, que percorre o tabuleiro, e quando encontra uma peça
do jogador atual chama o método `Board.CalculateMove()`, passando-lhe as
coordenadas da peça em questão e o número de movimentações a serem feitas.  
Por sua vez, o método `Board.CalculateMove()` irá avançar um campo de cada vez
até atingir o campo para onde a peça possivelmente se poderá mover. Depois
verifica se esse campo conta como um movimento válido, ou seja, se está livre ou
se está ocupado por uma peça adversária e não se trata de um campo seguro.  
Caso se verifique que é um movimento válido, é adicionada uma nova instância de
`Piece` à lista `availableMoves`. Se não se tratar de um movimento válido,
retorna ao método `Board.GetMoves()`, e o processo é repetido até que todo o
tabuleiro tenha sido percorrido.

A movimentação das peças é feita através do método `Board.MovePiece()`, que
recebe um _int_ que representa a jogada escolhida pelo jogador. Após decrementar
esse _int_, o mesmo passa a representar o _index_ da lista `availableMoves` onde
se encontra a peça que vai ser movida.  
Para realizar a movimentação são utilizadas operações _bitwise_, de modo a
ativar a peça no campo para onde se moveu, e a desativá-la do campo de onde saiu.  
Estas operações permitem-nos manter a informação acerca do tipo de campo do
tabuleiro, sendo este um dos tipos presentes na enumeração `Fields`.  
Após a movimentação da peça ser realizada, o método remove ainda todos os
elementos da lista `availableMoves`.

Após cada jogada verifica-se se alguém ganhou o jogo. Caso isso se verifique,
os jogadores são notificados de qual foi o vencedor e o _game loop_ acaba, caso
contrário o _game loop_ volta ao início e todo o processo descrito é repetido.

### Diagrama UML

![DiagramaUML](img/umlDiagram.png "Diagrama UML")

## Referências

- [Probability in and Ancient Game - SlideShare](https://pt.slideshare.net/rodecss/probability-in-and-ancient-game)
- [Document your C# code with XML comments - Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc)
- [How to use the try/catch block to catch exceptions - Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-use-the-try-catch-block-to-catch-exceptions)
- [Doxywizard usage - Doxygen](https://www.doxygen.nl/manual/doxywizard_usage.html)
- [How to make an introduction page with Doxygen - StackOverflow](https://stackoverflow.com/questions/9502426/how-to-make-an-introduction-page-with-doxygen/26244558#26244558)
