function  event_say(choice)
diagOptions = {}
    npcDialogue = "The temple has been studying the religious habits of the coldpaw gnolls. The gnolls inside are not hostile. I am considering a way to make a friendly gesture."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end