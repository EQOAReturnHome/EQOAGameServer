function  event_say(choice)
diagOptions = {}
    npcDialogue = "Warlord Jurglash will crush the puny froglok, it only a matter of time. Froglok small and no match for troll axe."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end