function  event_say(choice)
diagOptions = {}
    npcDialogue = "You finally found the bank! The rogue guild can be found upstairs. playerName, a word of caution, rogues make excellent tax collectors so watch your pockets."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end