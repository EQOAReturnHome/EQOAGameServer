function  event_say(choice)
diagOptions = {}
    npcDialogue = "If Hopper didn't send you then why would you want to bind here? Get out of this city before the mafia notices your presence."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end