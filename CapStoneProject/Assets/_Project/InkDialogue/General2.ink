INCLUDE Globals.ink

{General2Talked:
    -> AfterTalk
- else:
    -> Main
}

=== Main ===
Dimmi ragazzo, hai qualcosa per me?

{hasLetter:
    -> GiveMessage
- else:
    * Non ho niente
        -> NoHasLetter
}

=== GiveMessage ===
~General2Talked=true
{hasReadLetter:
    * Consegna lettera
        -> GiveReadLetter
    * Menti sul contenuto
        -> Lie
- else:
    * Dai la lettera
        -> GiveLetter
}

=== NoHasLetter ===
Torna indietro e recupera il messaggio.
-> END

=== GiveLetter ===
Ottimo soldato, vai a riposarti o a farti un giro nel campo.
~Final=1
-> END

=== GiveReadLetter ===
Ottimo soldato, vai a riposarti o a farti un giro nel campo.

Aspetta... la lettera è aperta.

Questa volta te la faccio passare liscia. Prossima volta ci saranno conseguenze.
~Final=1
-> END

=== Lie ===
Quindi il generale ha ordinato questo?

Non me lo aspettavo... ma chi sono io per contraddirlo.
~Final=2
-> END

=== AfterTalk ===
Hai già fatto quello che dovevi.

Sparisci. Vai a riposare o renditi utile altrove.
-> END