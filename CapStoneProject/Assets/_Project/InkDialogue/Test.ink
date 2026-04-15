INCLUDE GLobals.ink
{hasLetter:->Consegna|->NoLettera}
===Consegna===
Che vuoi Marmorchio?
*dai la lettera 
Bravo marmorchio.
{hasReadLetter:->LettoLettera}
->DONE
*menti sul contenuto della lettere
->END
===NoLettera===
Sparisci marmorchio
->END
===LettoLettera===
Marmochio hai letto la lettera, questa volta te la faccio passare liscia
->END