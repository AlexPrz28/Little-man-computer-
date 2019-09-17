# Little Man Computer

Interative Little Man  Computer program in C# using Windows Forms.

## Getting Started

The Little Man Computer contents:
- An input area: area where you input the data required for your program.
- An output list: area where outputs the current calculator number.
- A positive, zero and interrupt flag.
- Console: shows a description of the current instruction executing.
- Calcultator: area asignated to place the input data or the load data in the mailboxes.
- Instruction counter: shows the current instruction running
- Mailboxes: address where the data is stored.
- Run, reset, step-by-step and interrupt button


These instructions sets are the only commands that the system requires to run. 

| Instruction | Mnemonic | Machine code | Info | Example |
| ------------- | ------------- | ------------- | ------------- |------------- |
| End | HLT | 000 | Ends the program | 000 |
| Add | ADD | 1XX | Adds the value at mailbox address xx to the calculator | 125 |
| Substract | SUB | 2XX | Substracts the value at mailbox address xx from the calculator | 210 |
| Store | STA | 3XX | Store contents of calculator to mailbox address xx | 306 |
| Load | LDA | 5XX | Loads contents of mailbox address xx to the calculator | 509 |
| Branch always | BRA | 6XX | Jumps to the instruction at mailbox address xx | 623 |
| Branch if zero | BRZ | 7XX | Jumps to the instruction at mailbox address xx, given the value in the calculator is 0 | 713 |
| Branch if positive | BRP | 8XX | Jumps to the instruction at mailbox address xx, given the value in the accumulator is 0, or greater | 812 |
| Input | INP | 901 | Prompts user for input, store in calculator | 901 |
| Output | OUT | 902 | Outputs value currently in calculator | 902 |
| Finish Interrupt | FIN | 999 | Jumps back to the last instruction set before the interrupt was clicked | 999 |




### Installing

* Download the repository in your computer, unzipped it and open the ".sln" file 
* If you want to open the ".exe" file open bin/Debug/act4_LMC.exe



## How to use the Little Man Computer 


* The Little Man Computer has 99 spaces called mailboxes, the instructions have to be written in the addresses manually and must contain 3 numbers (the instruction and additional data). You can add only 2 numbers if you want to store default data for your program
* The "Run" button starts the program, it fetches and executes every instruction that you input until the instruction is "000"
* The "step-by-step" button fetches and executes the instructions indivually, you have to keep clicking the button until the program finds a "000" and stops the tasks.
* The "Interrupt " button activates the interrupt flag and starts running the interrupt instructions previously add in the mailboxes.
* The "Reset" button erase all the data in the mailboxes.

### Indications

* The zero flag turns on if the operation made gives a zero result showed the calculator.
* The positive flag will indicate if the number in the calculator is positive or negative.
* The interrupt flag will turn on when the interrupt button is pressed.



## Built With

* Microsoft Visual studio 2019


## Authors

* **Santiago Alcérreca** 
* **Alejandro Pérez-Gómez** 




