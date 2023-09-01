function event_say()
diagOptions = {}
    npcDialogue = "I know most erudites don't like to get their hands dirty with tradeskills but it is my belief that we import to much equipment."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end