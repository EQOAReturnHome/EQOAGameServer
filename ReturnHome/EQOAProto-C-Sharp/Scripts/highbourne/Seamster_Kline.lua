function event_say()
diagOptions = {}
    npcDialogue = "The skill of tailoring is not one pursued by many though I maintain it is an art and requires impeccable skill and attention to detail to master."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end