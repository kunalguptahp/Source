CREATE TABLE STAGE_ATTRIBUTE
(
  ATT_ID       int             NOT NULL,
  ATT_KEY      NVARCHAR(16)     NOT NULL,
  DESCRIPTION  NVARCHAR(64)
)

CREATE TABLE STAGE_CONTENT
(
  CONTENT_ID       int                       NOT NULL,
  CONTENT_ITEM_ID  int                       NOT NULL
)

CREATE TABLE STAGE_CONTENT_ITEM
(
  CONTENT_ITEM_ID  int                       NOT NULL,
  CONTENT_KEY      NVARCHAR(64)            NOT NULL,
  CONTENT_VALUE    NVARCHAR(600)           NOT NULL
)

CREATE TABLE STAGE_SELECTOR_GROUP
(
  GROUP_ID          int                      NOT NULL,
  SELECTOR_ITEM_ID  int                      NOT NULL
)

CREATE TABLE STAGE_SELECTOR_ITEM
(
  SELECTOR_ITEM_ID  int                      NOT NULL,
  ATT_ID            int                      NOT NULL,
  ATT_VALUE         NVARCHAR(64)           NOT NULL
)

CREATE TABLE STAGE_TARGET
(
  TARGET_ID         int                      NOT NULL,
  SITE_ID           int                      NOT NULL,
  GROUP_ID          int                      NOT NULL,
  CONTENT_ID        int                      NOT NULL,
  CREATION_DATE     NVARCHAR(100)            NOT NULL,
  LAST_UPDATE_DATE  NVARCHAR(100)            NOT NULL,
  VERSION           NVARCHAR(10)			 DEFAULT 1                     NOT NULL,
  MESSAGE_FLAG_ID   int                   DEFAULT 1                     NOT NULL,
  DESCRIPTION       NVARCHAR(64),
  GROUP_TYPE_ID     int                      NOT NULL
)

CREATE TABLE STAGE_TARGET_SERVER
(
  SITE_ID      int                        NOT NULL,
  SITE_NAME    NVARCHAR(64)               NOT NULL,
  DESCRIPTION  NVARCHAR(64),
  SITE_CODE    NVARCHAR(20)
)

CREATE TABLE STAGE_GROUP_TYPE
(
  GROUP_TYPE_ID   int,
  GROUP_TYPE_KEY  NVARCHAR(64),
  DESCRIPTION     NVARCHAR(64)
)

CREATE TABLE STAGE_GROUP_TYPE_DEFINITION
(
  GROUP_TYPE_ID  int,
  ATT_ID         int
)

CREATE TABLE STAGE_TARGET_SERVER_GROUP_TYPE
(
  SITE_ID        int,
  GROUP_TYPE_ID  int
)
