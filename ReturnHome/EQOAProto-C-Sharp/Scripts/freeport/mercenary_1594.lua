function  event_say(choice)
diagOptions = {}
    npcDialogue = "Make one wrong move in House Slaerin, and you'll find yourself surrounded by Freeport mercenaries. "
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end