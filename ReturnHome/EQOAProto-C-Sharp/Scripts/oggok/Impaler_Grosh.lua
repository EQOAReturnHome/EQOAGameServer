function  event_say(choice)
diagOptions = {}
    npcDialogue = "This is entrance to Temple of Greenblood. If you enter this place of dark magic, the dark magic may follow you till the end of your days."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end