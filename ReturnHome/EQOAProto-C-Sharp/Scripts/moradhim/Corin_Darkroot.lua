function  event_say(choice)
diagOptions = {}
    npcDialogue = "Ma' wife Pona and I fled here after the orc invasion of the Druk mines in the east. We barely escaped with our lives. The good people of Moradhim were kind enough to take us in."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end