function  event_say(choice)
diagOptions = {}
    npcDialogue = "Dees da softest beds in all of Grobb. Sleep Place usually for visitors from Neriak or Oggok. playerName can sleep too, for many coin."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end