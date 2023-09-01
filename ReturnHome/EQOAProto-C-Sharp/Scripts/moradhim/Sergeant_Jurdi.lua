function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to the grand dwarf city of Moradhim!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end