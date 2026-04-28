INCLUDE Globals.ink
->Main
=== Main ===

{sideQuestDone:
    -> Finished
- else:
    {sideQuestAccepted:
        -> QuestActive
    - else:
        -> FirstMeet
    }
}

=== FirstMeet ===
Ehi... tu.

Puoi aiutarmi?

In cambio ti dirò cose sulla guerra che nessun altro sa.

Ho bisogno di qualcuno che raccolga delle cose per me.

Sono sparse nel campo minato.

Tre medagliette. Niente di più.

Mi aiuti?

* Sì.
    ~ sideQuestAccepted = true
    Perfetto. Non deludermi.
    -> END

* No.
    Peccato. Pensavo che eri diverso dagli altri.
    -> END

=== QuestActive ===
Eccoti! Fa vedere quanti ne hai raccolti.
Ne hai {sideQuestItems} su 3.

{sideQuestItems == 0:
    Non hai ancora trovato nulla.
- else:
    {sideQuestItems == 1:
        Uno solo? Non basta.
    - else:
        {sideQuestItems == 2:
            Sei a metà strada.
        - else:
            Hai trovato tutto!
            -> CompleteQuest
        }
    }
}

-> END

=== CompleteQuest ===
Grazie... davvero, messaggero.
Queste piastrine... pesano più di quanto immagini.
Erano dei miei compagni. 
Ora, come promesso, ti dirò la verità. Ma attento
la verità è un veleno che non tutti sanno gestire.

Questa guerra È una menzogna. Non è nata da un’invasione, né da un attacco a sorpresa. 
È nata da un ordine.
Trent'anni fa, mentre noi morivamo al fronte credendo di difendere i confini, i generali stavano solo alzando un sipario di fumo.
Non proteggevano il territorio... nascondevano un peccato. Qualcosa che doveva restare sepolto nel buio per sempre.
Tutto ebbe inizio con uno scavo non autorizzato nel Settore Nord. 
Dicono che lì sotto abbiano trovato l'impossibile.
Qualcosa che non appartiene a questo mondo, né a nessuna delle due fazioni che si scannano da decenni. 
Chi ha visto il fondo di quel cratere non è più tornato, o se lo ha fatto, ha perso la voce e la ragione.
All'inizio, le due fazioni erano alleate. Poi, tra quelle pareti di roccia e fango, è successo qualcosa. 
Un tradimento? Una scoperta troppo grande per essere divisa? Non so cosa si siano detti in quelle stanze segrete, 
ma da quell'istante la pace è diventata un ricordo e la guerra l'unica via d'uscita.
E la parte peggiore? Non è mai finita. Quello che hanno svegliato nel Settore Nord è ancora lì, che aspetta.
Questo è tutto quello che so, spero ti possa tornare utile.
~ sideQuestDone = true
-> END

=== Finished ===
Hai già fatto tutto.

Ora sai abbastanza. Non farti vedere troppo curioso.
-> END