function event_say()
diagOptions = {}
    npcDialogue = "We must protect Tethelin with all we have, and never let it fall. The threat may be a malignant pest in nature, or a wicked enemy faction. We must keep our eyes peeled, and our quivers stocked. Our very race depends on it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end